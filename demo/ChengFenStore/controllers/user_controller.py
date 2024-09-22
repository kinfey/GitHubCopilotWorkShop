from flask import Blueprint, request, jsonify
from services.user_service import UserService

user_bp = Blueprint('user', __name__)

@user_bp.route('/register', methods=['POST'])
def register_user():
    data = request.get_json()
    phone_number = data.get('phone_number')
    name = data.get('name')
    password = data.get('password')
    verification_code = data.get('verification_code')
    result = UserService.register(phone_number, name, password, verification_code)
    return jsonify(result)

@user_bp.route('/login', methods=['POST'])
def login_user():
    data = request.get_json()
    phone_number = data.get('phone_number')
    password = data.get('password')
    verification_code = data.get('verification_code')
    result = UserService.login(phone_number, password, verification_code)
    return jsonify(result)

@user_bp.route('/profile', methods=['GET'])
def get_user_profile():
    phone_number = request.args.get('phone_number')
    result = UserService.get_user_profile(phone_number)
    return jsonify(result)
