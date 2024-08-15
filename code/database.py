from flask_sqlalchemy import SQLAlchemy

db = SQLAlchemy()

def init_db(app):
    db.init_app(app)

def create_tables():
    db.create_all()

def drop_tables():
    db.drop_all()

def seed_db():
    from models import User, Order, Payment
    from datetime import datetime

    # Add initial users
    user1 = User(phone_number="1234567890", name="John Doe", password="password")
    user2 = User(phone_number="0987654321", name="Jane Doe", password="password")
    db.session.add(user1)
    db.session.add(user2)

    # Add initial orders
    order1 = Order(user_id=1, product_details="Product 1", order_status="待支付", created_at=datetime.utcnow())
    order2 = Order(user_id=2, product_details="Product 2", order_status="待支付", created_at=datetime.utcnow())
    db.session.add(order1)
    db.session.add(order2)

    # Add initial payments
    payment1 = Payment(order_id=1, payment_method="微信支付", payment_status="未支付", created_at=datetime.utcnow())
    payment2 = Payment(order_id=2, payment_method="支付宝", payment_status="未支付", created_at=datetime.utcnow())
    db.session.add(payment1)
    db.session.add(payment2)

    db.session.commit()
