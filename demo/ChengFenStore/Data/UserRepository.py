from ..Data.AppDbContext import db, User

class UserRepository:
    def add_user(self, user):
        db.session.add(user)
        db.session.commit()

    def get_user_by_phone_number(self, phone_number):
        return User.query.filter_by(phone_number=phone_number).first()
