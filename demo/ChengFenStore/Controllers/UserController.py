from flask import Flask, request, jsonify, Blueprint
from ..Services.UserService import UserService

user_controller = Blueprint('user_controller', __name__)

@user_controller.route('/register', methods=['POST'])
def register():
    data = request.get_json()
    result = UserService.register(data)
    if result['success']:
        return jsonify(result), 200
    return jsonify(result), 400

@user_controller.route('/login', methods=['POST'])
def login():
    data = request.get_json()
    result = UserService.login(data)
    if result['success']:
        return jsonify(result), 200
    return jsonify(result), 401

@user_controller.route('/profile', methods=['GET'])
def get_user_profile():
    user_id = request.headers.get('user_id')
    user_profile = UserService.get_user_profile(user_id)
    if user_profile:
        return jsonify(user_profile), 200
    return jsonify({'message': 'User not found'}), 404
