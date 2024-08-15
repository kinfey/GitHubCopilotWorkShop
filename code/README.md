# 肠粉店管理系统

这是一个肠粉店管理系统，包含用户注册与认证、订单管理、支付系统、以及后台管理系统。

## 设置和运行项目

1. 克隆仓库到本地
   ```bash
   git clone <repository-url>
   cd <repository-directory>
   ```

2. 创建并激活虚拟环境
   ```bash
   python -m venv venv
   source venv/bin/activate  # 对于 Windows 系统，使用 `venv\Scripts\activate`
   ```

3. 安装项目依赖
   ```bash
   pip install -r requirements.txt
   ```

4. 初始化数据库
   ```bash
   flask db init
   flask db migrate
   flask db upgrade
   ```

5. 运行 Flask 应用
   ```bash
   flask run
   ```

## 运行测试

1. 确保虚拟环境已激活并安装了所有依赖
2. 运行测试
   ```bash
   pytest
   ```

## 使用 API 端点

### 用户注册与认证

- **注册**
  - URL: `/register`
  - 方法: `POST`
  - 请求体:
    ```json
    {
      "phone_number": "string",
      "name": "string",
      "password": "string"
    }
    ```
  - 响应:
    ```json
    {
      "message": "User registered successfully"
    }
    ```

- **登录**
  - URL: `/login`
  - 方法: `POST`
  - 请求体:
    ```json
    {
      "phone_number": "string",
      "password": "string"
    }
    ```
  - 响应:
    ```json
    {
      "access_token": "string"
    }
    ```

### 订单管理

- **查看菜单**
  - URL: `/menu`
  - 方法: `GET`
  - 响应:
    ```json
    [
      {
        "product_id": "integer",
        "name": "string",
        "description": "string",
        "price": "float",
        "image_url": "string"
      }
    ]
    ```

- **创建订单**
  - URL: `/orders`
  - 方法: `POST`
  - 请求体:
    ```json
    {
      "user_id": "integer",
      "products": [
        {
          "product_id": "integer",
          "quantity": "integer"
        }
      ],
      "delivery_method": "string",
      "delivery_time": "string",
      "delivery_address": "string"
    }
    ```
  - 响应:
    ```json
    {
      "order_id": "integer",
      "message": "Order created successfully"
    }
    ```

- **查看订单状态**
  - URL: `/orders/<order_id>`
  - 方法: `GET`
  - 响应:
    ```json
    {
      "order_id": "integer",
      "status": "string"
    }
    ```

### 支付系统

- **支付订单**
  - URL: `/payments`
  - 方法: `POST`
  - 请求体:
    ```json
    {
      "order_id": "integer",
      "payment_method": "string"
    }
    ```
  - 响应:
    ```json
    {
      "payment_id": "integer",
      "message": "Payment successful"
    }
    ```

### 后台管理系统

- **管理用户**
  - URL: `/admin/users`
  - 方法: `GET`
  - 响应:
    ```json
    [
      {
        "user_id": "integer",
        "phone_number": "string",
        "name": "string"
      }
    ]
    ```

- **管理订单**
  - URL: `/admin/orders`
  - 方法: `GET`
  - 响应:
    ```json
    [
      {
        "order_id": "integer",
        "user_id": "integer",
        "status": "string"
      }
    ]
    ```

- **管理支付记录**
  - URL: `/admin/payments`
  - 方法: `GET`
  - 响应:
    ```json
    [
      {
        "payment_id": "integer",
        "order_id": "integer",
        "payment_method": "string",
        "status": "string"
      }
    ]
    ```
