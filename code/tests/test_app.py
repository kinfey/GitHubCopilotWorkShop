import pytest
from app import app, db, User, Order, Payment

@pytest.fixture
def client():
    app.config['TESTING'] = True
    app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///:memory:'
    with app.test_client() as client:
        with app.app_context():
            db.create_all()
        yield client
        with app.app_context():
            db.drop_all()

def test_register(client):
    response = client.post('/register', json={
        'phone_number': '1234567890',
        'name': 'John Doe',
        'password': 'password'
    })
    assert response.status_code == 200
    assert response.json['phone_number'] == '1234567890'

def test_login(client):
    client.post('/register', json={
        'phone_number': '1234567890',
        'name': 'John Doe',
        'password': 'password'
    })
    response = client.post('/login', json={
        'phone_number': '1234567890',
        'password': 'password'
    })
    assert response.status_code == 200
    assert 'access_token' in response.json

def test_create_order(client):
    client.post('/register', json={
        'phone_number': '1234567890',
        'name': 'John Doe',
        'password': 'password'
    })
    login_response = client.post('/login', json={
        'phone_number': '1234567890',
        'password': 'password'
    })
    access_token = login_response.json['access_token']
    response = client.post('/orders', json={
        'product_details': 'Product 1'
    }, headers={'Authorization': f'Bearer {access_token}'})
    assert response.status_code == 200
    assert response.json['product_details'] == 'Product 1'

def test_get_orders(client):
    client.post('/register', json={
        'phone_number': '1234567890',
        'name': 'John Doe',
        'password': 'password'
    })
    login_response = client.post('/login', json={
        'phone_number': '1234567890',
        'password': 'password'
    })
    access_token = login_response.json['access_token']
    client.post('/orders', json={
        'product_details': 'Product 1'
    }, headers={'Authorization': f'Bearer {access_token}'})
    response = client.get('/orders', headers={'Authorization': f'Bearer {access_token}'})
    assert response.status_code == 200
    assert len(response.json) == 1

def test_create_payment(client):
    client.post('/register', json={
        'phone_number': '1234567890',
        'name': 'John Doe',
        'password': 'password'
    })
    login_response = client.post('/login', json={
        'phone_number': '1234567890',
        'password': 'password'
    })
    access_token = login_response.json['access_token']
    client.post('/orders', json={
        'product_details': 'Product 1'
    }, headers={'Authorization': f'Bearer {access_token}'})
    response = client.post('/payments', json={
        'order_id': 1,
        'payment_method': '微信支付'
    }, headers={'Authorization': f'Bearer {access_token}'})
    assert response.status_code == 200
    assert response.json['payment_method'] == '微信支付'

def test_get_payments(client):
    client.post('/register', json={
        'phone_number': '1234567890',
        'name': 'John Doe',
        'password': 'password'
    })
    login_response = client.post('/login', json={
        'phone_number': '1234567890',
        'password': 'password'
    })
    access_token = login_response.json['access_token']
    client.post('/orders', json={
        'product_details': 'Product 1'
    }, headers={'Authorization': f'Bearer {access_token}'})
    client.post('/payments', json={
        'order_id': 1,
        'payment_method': '微信支付'
    }, headers={'Authorization': f'Bearer {access_token}'})
    response = client.get('/payments', headers={'Authorization': f'Bearer {access_token}'})
    assert response.status_code == 200
    assert len(response.json) == 1

def test_get_users(client):
    client.post('/register', json={
        'phone_number': '1234567890',
        'name': 'John Doe',
        'password': 'password'
    })
    login_response = client.post('/login', json={
        'phone_number': '1234567890',
        'password': 'password'
    })
    access_token = login_response.json['access_token']
    response = client.get('/admin/users', headers={'Authorization': f'Bearer {access_token}'})
    assert response.status_code == 200
    assert len(response.json) == 1

def test_get_all_orders(client):
    client.post('/register', json={
        'phone_number': '1234567890',
        'name': 'John Doe',
        'password': 'password'
    })
    login_response = client.post('/login', json={
        'phone_number': '1234567890',
        'password': 'password'
    })
    access_token = login_response.json['access_token']
    client.post('/orders', json={
        'product_details': 'Product 1'
    }, headers={'Authorization': f'Bearer {access_token}'})
    response = client.get('/admin/orders', headers={'Authorization': f'Bearer {access_token}'})
    assert response.status_code == 200
    assert len(response.json) == 1

def test_get_all_payments(client):
    client.post('/register', json={
        'phone_number': '1234567890',
        'name': 'John Doe',
        'password': 'password'
    })
    login_response = client.post('/login', json={
        'phone_number': '1234567890',
        'password': 'password'
    })
    access_token = login_response.json['access_token']
    client.post('/orders', json={
        'product_details': 'Product 1'
    }, headers={'Authorization': f'Bearer {access_token}'})
    client.post('/payments', json={
        'order_id': 1,
        'payment_method': '微信支付'
    }, headers={'Authorization': f'Bearer {access_token}'})
    response = client.get('/admin/payments', headers={'Authorization': f'Bearer {access_token}'})
    assert response.status_code == 200
    assert len(response.json) == 1
