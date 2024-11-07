from ..Data.AppDbContext import db

class Order(db.Model):
    order_id = db.Column(db.Integer, primary_key=True)
    user_id = db.Column(db.String(20), db.ForeignKey('user.phone_number'), nullable=False)
    product_details = db.Column(db.String(200), nullable=False)
    delivery_method = db.Column(db.String(50), nullable=False)
    order_status = db.Column(db.String(50), nullable=False)
    order_status_history = db.relationship('OrderStatus', backref='order', lazy=True)
