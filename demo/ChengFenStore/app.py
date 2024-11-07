from flask import Flask, request, jsonify, Blueprint
from flask_sqlalchemy import SQLAlchemy
from flask_jwt_extended import JWTManager
from .Controllers.OrderController import order_controller
from .Controllers.PaymentController import payment_controller
from .Controllers.UserController import user_controller

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///chengfenstore.db'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
app.config['JWT_SECRET_KEY'] = 'your_jwt_secret_key'

db = SQLAlchemy(app)
jwt = JWTManager(app)

app.register_blueprint(order_controller)
app.register_blueprint(payment_controller)
app.register_blueprint(user_controller)

if __name__ == '__main__':
    app.run(debug=True)
