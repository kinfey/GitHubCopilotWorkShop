from ..Data.AppDbContext import db, Payment

class PaymentRepository:
    def add_payment(self, payment):
        db.session.add(payment)
        db.session.commit()

    def get_payments(self):
        return Payment.query.all()

    def get_payment_by_id(self, payment_id):
        return Payment.query.get(payment_id)
