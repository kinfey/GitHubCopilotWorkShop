from flask import Flask, request, jsonify, Blueprint
from ..Services.OrderService import OrderService

order_controller = Blueprint('order_controller', __name__)

@order_controller.route('/api/order/create', methods=['POST'])
def create_order():
    order_data = request.get_json()
    order_service = OrderService()
    created_order = order_service.create_order(order_data)
    return jsonify(created_order), 201

@order_controller.route('/api/order/<int:id>', methods=['GET'])
def get_order_by_id(id):
    order_service = OrderService()
    order = order_service.get_order_by_id(id)
    if order is None:
        return jsonify({'message': 'Order not found'}), 404
    return jsonify(order), 200

@order_controller.route('/api/order/confirm/<int:id>', methods=['PUT'])
def confirm_order(id):
    order_service = OrderService()
    order = order_service.confirm_order(id)
    if order is None:
        return jsonify({'message': 'Order not found'}), 404
    return jsonify(order), 200

@order_controller.route('/api/order/track/<int:id>', methods=['GET'])
def track_order(id):
    order_service = OrderService()
    order = order_service.track_order(id)
    if order is None:
        return jsonify({'message': 'Order not found'}), 404
    return jsonify(order), 200

def register_order_controller(app):
    app.register_blueprint(order_controller)
