from flask import Flask, request, jsonify, Blueprint
from ..Services.PaymentService import PaymentService

payment_controller = Blueprint('payment_controller', __name__)

@payment_controller.route('/api/payment/process', methods=['POST'])
def process_payment():
    payment_data = request.get_json()
    payment_service = PaymentService()
    result = payment_service.process_payment(payment_data)
    if result:
        return jsonify({'message': 'Payment processed successfully'}), 200
    return jsonify({'message': 'Payment processing failed'}), 400

@payment_controller.route('/api/payment/records', methods=['GET'])
def get_payment_records():
    payment_service = PaymentService()
    records = payment_service.get_payment_records()
    return jsonify(records), 200

def register_payment_controller(app):
    app.register_blueprint(payment_controller)
