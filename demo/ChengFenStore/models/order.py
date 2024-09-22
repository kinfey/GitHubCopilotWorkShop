class Order:
    def __init__(self, order_id, user_id, product_details, delivery_method, order_status, order_status_history):
        self.order_id = order_id
        self.user_id = user_id
        self.product_details = product_details
        self.delivery_method = delivery_method
        self.order_status = order_status
        self.order_status_history = order_status_history
