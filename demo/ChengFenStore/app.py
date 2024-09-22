from flask import Flask
from controllers.order_controller import order_bp
from controllers.payment_controller import payment_bp
from controllers.user_controller import user_bp
from data.db import init_db

app = Flask(__name__)

app.register_blueprint(order_bp, url_prefix='/orders')
app.register_blueprint(payment_bp, url_prefix='/payments')
app.register_blueprint(user_bp, url_prefix='/users')

init_db(app)

if __name__ == '__main__':
    app.run(debug=True)
