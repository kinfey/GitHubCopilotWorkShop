from ..Data.AppDbContext import db, Payment

class PaymentService:
    def process_payment(self, payment_data):
        payment = Payment(**payment_data)
        payment.payment_status = "Processed"
        db.session.add(payment)
        db.session.commit()
        return True

    def get_payment_records(self):
        return Payment.query.all()
