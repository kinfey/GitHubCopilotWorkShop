from ..Data.AppDbContext import db, Order

class OrderService:
    def create_order(self, order_data):
        order = Order(**order_data)
        db.session.add(order)
        db.session.commit()
        return order

    def get_order_by_id(self, order_id):
        return Order.query.get(order_id)

    def confirm_order(self, order_id):
        order = Order.query.get(order_id)
        if order:
            order.order_status = "已支付"
            db.session.commit()
        return order

    def track_order(self, order_id):
        return Order.query.get(order_id)
