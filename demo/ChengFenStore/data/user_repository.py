class UserRepository:
    def __init__(self, db):
        self.db = db

    def add_user(self, user):
        self.db.session.add(user)
        self.db.session.commit()

    def get_user_by_phone_number(self, phone_number):
        return self.db.session.query(User).filter_by(phone_number=phone_number).first()
