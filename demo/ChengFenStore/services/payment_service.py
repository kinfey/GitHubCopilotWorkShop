class PaymentService:
    def __init__(self, payment_repository):
        self.payment_repository = payment_repository

    def process_payment(self, payment):
        # Implement payment processing logic here
        payment.payment_status = "Processed"
        self.payment_repository.add_payment(payment)
        return True

    def get_payment_records(self):
        return self.payment_repository.get_all_payments()
