class OrderService:
    def __init__(self, order_repository):
        self.order_repository = order_repository

    def create_order(self, order):
        self.order_repository.add_order(order)
        return order

    def get_order_by_id(self, order_id):
        return self.order_repository.get_order_by_id(order_id)

    def confirm_order(self, order_id):
        order = self.order_repository.get_order_by_id(order_id)
        if order:
            order.order_status = "已支付"
            self.order_repository.update_order(order)
        return order

    def track_order(self, order_id):
        return self.order_repository.get_order_by_id(order_id)
