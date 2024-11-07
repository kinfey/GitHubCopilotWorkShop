from flask_sqlalchemy import SQLAlchemy

db = SQLAlchemy()

class User(db.Model):
    phone_number = db.Column(db.String(20), primary_key=True)
    name = db.Column(db.String(100), nullable=False)
    password = db.Column(db.String(100), nullable=False)

class Order(db.Model):
    order_id = db.Column(db.Integer, primary_key=True)
    user_id = db.Column(db.String(20), db.ForeignKey('user.phone_number'), nullable=False)
    product_details = db.Column(db.String(200), nullable=False)
    delivery_method = db.Column(db.String(50), nullable=False)
    order_status = db.Column(db.String(50), nullable=False)
    order_status_history = db.relationship('OrderStatus', backref='order', lazy=True)

class Payment(db.Model):
    payment_id = db.Column(db.Integer, primary_key=True)
    order_id = db.Column(db.Integer, db.ForeignKey('order.order_id'), nullable=False)
    payment_method = db.Column(db.String(50), nullable=False)
    payment_status = db.Column(db.String(50), nullable=False)
    payment_status_history = db.relationship('PaymentStatus', backref='payment', lazy=True)

def init_db(app):
    db.init_app(app)
    with app.app_context():
        db.create_all()
