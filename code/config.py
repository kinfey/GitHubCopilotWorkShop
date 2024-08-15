class Config:
    SECRET_KEY = 'super-secret'  # Change this to a random secret key
    SQLALCHEMY_DATABASE_URI = 'sqlite:///app.db'
    SQLALCHEMY_TRACK_MODIFICATIONS = False
    JWT_SECRET_KEY = 'super-secret'  # Change this to a random secret key
    PAYMENT_GATEWAYS = {
        'wechat': {
            'api_key': 'your_wechat_api_key',
            'api_secret': 'your_wechat_api_secret'
        },
        'alipay': {
            'api_key': 'your_alipay_api_key',
            'api_secret': 'your_alipay_api_secret'
        },
        'bank': {
            'api_key': 'your_bank_api_key',
            'api_secret': 'your_bank_api_secret'
        }
    }
