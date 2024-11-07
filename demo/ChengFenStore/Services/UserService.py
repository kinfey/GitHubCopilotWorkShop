from ..Data.AppDbContext import db, User
from flask_jwt_extended import create_access_token
from datetime import timedelta

class UserService:
    @staticmethod
    def register(data):
        phone_number = data.get('phone_number')
        name = data.get('name')
        password = data.get('password')

        if User.query.filter_by(phone_number=phone_number).first():
            return {'success': False, 'message': 'User already exists.'}

        new_user = User(phone_number=phone_number, name=name, password=password)
        db.session.add(new_user)
        db.session.commit()

        return {'success': True, 'message': 'User registered successfully.'}

    @staticmethod
    def login(data):
        phone_number = data.get('phone_number')
        password = data.get('password')

        user = User.query.filter_by(phone_number=phone_number, password=password).first()
        if not user:
            return {'success': False, 'message': 'Invalid phone number or password.'}

        token = UserService.generate_jwt_token(user)
        return {'success': True, 'message': 'Login successful.', 'token': token}

    @staticmethod
    def generate_jwt_token(user):
        expires = timedelta(days=7)
        token = create_access_token(identity=user.phone_number, expires_delta=expires)
        return token
