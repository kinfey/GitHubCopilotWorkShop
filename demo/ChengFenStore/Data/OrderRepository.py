from ..Data.AppDbContext import db, Order

class OrderRepository:
    def add_order(self, order):
        db.session.add(order)
        db.session.commit()

    def get_order_by_id(self, order_id):
        return Order.query.get(order_id)

    def update_order(self, order):
        db.session.merge(order)
        db.session.commit()
