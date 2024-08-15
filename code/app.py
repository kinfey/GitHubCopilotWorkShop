from flask import Flask, request, jsonify
from flask_sqlalchemy import SQLAlchemy
from flask_marshmallow import Marshmallow
from flask_jwt_extended import JWTManager, create_access_token, jwt_required, get_jwt_identity
from datetime import datetime, timedelta
import os

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///app.db'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
app.config['JWT_SECRET_KEY'] = 'super-secret'  # Change this to a random secret key

db = SQLAlchemy(app)
ma = Marshmallow(app)
jwt = JWTManager(app)

# User Model
class User(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    phone_number = db.Column(db.String(20), unique=True, nullable=False)
    name = db.Column(db.String(100), nullable=False)
    password = db.Column(db.String(100), nullable=False)

# Order Model
class Order(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    user_id = db.Column(db.Integer, db.ForeignKey('user.id'), nullable=False)
    product_details = db.Column(db.String(200), nullable=False)
    order_status = db.Column(db.String(50), nullable=False, default='待支付')
    created_at = db.Column(db.DateTime, default=datetime.utcnow)

# Payment Model
class Payment(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    order_id = db.Column(db.Integer, db.ForeignKey('order.id'), nullable=False)
    payment_method = db.Column(db.String(50), nullable=False)
    payment_status = db.Column(db.String(50), nullable=False, default='未支付')
    created_at = db.Column(db.DateTime, default=datetime.utcnow)

# User Schema
class UserSchema(ma.SQLAlchemyAutoSchema):
    class Meta:
        model = User

# Order Schema
class OrderSchema(ma.SQLAlchemyAutoSchema):
    class Meta:
        model = Order

# Payment Schema
class PaymentSchema(ma.SQLAlchemyAutoSchema):
    class Meta:
        model = Payment

user_schema = UserSchema()
users_schema = UserSchema(many=True)
order_schema = OrderSchema()
orders_schema = OrderSchema(many=True)
payment_schema = PaymentSchema()
payments_schema = PaymentSchema(many=True)

# Initialize the database
@app.before_first_request
def create_tables():
    db.create_all()

# User Registration
@app.route('/register', methods=['POST'])
def register():
    phone_number = request.json['phone_number']
    name = request.json['name']
    password = request.json['password']

    new_user = User(phone_number=phone_number, name=name, password=password)
    db.session.add(new_user)
    db.session.commit()

    return user_schema.jsonify(new_user)

# User Login
@app.route('/login', methods=['POST'])
def login():
    phone_number = request.json['phone_number']
    password = request.json['password']

    user = User.query.filter_by(phone_number=phone_number, password=password).first()

    if user:
        access_token = create_access_token(identity={'phone_number': user.phone_number})
        return jsonify(access_token=access_token)
    else:
        return jsonify(message="Invalid credentials"), 401

# Order Management
@app.route('/orders', methods=['POST'])
@jwt_required()
def create_order():
    user_identity = get_jwt_identity()
    user = User.query.filter_by(phone_number=user_identity['phone_number']).first()

    product_details = request.json['product_details']
    new_order = Order(user_id=user.id, product_details=product_details)

    db.session.add(new_order)
    db.session.commit()

    return order_schema.jsonify(new_order)

@app.route('/orders', methods=['GET'])
@jwt_required()
def get_orders():
    user_identity = get_jwt_identity()
    user = User.query.filter_by(phone_number=user_identity['phone_number']).first()

    orders = Order.query.filter_by(user_id=user.id).all()
    return orders_schema.jsonify(orders)

@app.route('/orders/<int:id>', methods=['GET'])
@jwt_required()
def get_order(id):
    order = Order.query.get(id)
    return order_schema.jsonify(order)

@app.route('/orders/<int:id>', methods=['PUT'])
@jwt_required()
def update_order(id):
    order = Order.query.get(id)

    order.order_status = request.json['order_status']

    db.session.commit()
    return order_schema.jsonify(order)

# Payment Processing
@app.route('/payments', methods=['POST'])
@jwt_required()
def create_payment():
    order_id = request.json['order_id']
    payment_method = request.json['payment_method']

    new_payment = Payment(order_id=order_id, payment_method=payment_method)

    db.session.add(new_payment)
    db.session.commit()

    return payment_schema.jsonify(new_payment)

@app.route('/payments', methods=['GET'])
@jwt_required()
def get_payments():
    payments = Payment.query.all()
    return payments_schema.jsonify(payments)

@app.route('/payments/<int:id>', methods=['GET'])
@jwt_required()
def get_payment(id):
    payment = Payment.query.get(id)
    return payment_schema.jsonify(payment)

@app.route('/payments/<int:id>', methods=['PUT'])
@jwt_required()
def update_payment(id):
    payment = Payment.query.get(id)

    payment.payment_status = request.json['payment_status']

    db.session.commit()
    return payment_schema.jsonify(payment)

# Backend Management
@app.route('/admin/users', methods=['GET'])
@jwt_required()
def get_users():
    users = User.query.all()
    return users_schema.jsonify(users)

@app.route('/admin/orders', methods=['GET'])
@jwt_required()
def get_all_orders():
    orders = Order.query.all()
    return orders_schema.jsonify(orders)

@app.route('/admin/payments', methods=['GET'])
@jwt_required()
def get_all_payments():
    payments = Payment.query.all()
    return payments_schema.jsonify(payments)

if __name__ == '__main__':
    app.run(debug=True)
