class PaymentRepository:
    def __init__(self, db):
        self.db = db

    def add_payment(self, payment):
        self.db.session.add(payment)
        self.db.session.commit()

    def get_payment_by_id(self, payment_id):
        return self.db.session.query(Payment).filter_by(payment_id=payment_id).first()

    def update_payment(self, payment):
        self.db.session.commit()
