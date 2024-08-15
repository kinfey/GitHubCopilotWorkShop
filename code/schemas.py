from marshmallow import Schema, fields, validate

class UserSchema(Schema):
    id = fields.Int(dump_only=True)
    phone_number = fields.Str(required=True, validate=validate.Length(min=10, max=20))
    name = fields.Str(required=True, validate=validate.Length(min=1, max=100))
    password = fields.Str(required=True, load_only=True, validate=validate.Length(min=6, max=100))

class OrderSchema(Schema):
    id = fields.Int(dump_only=True)
    user_id = fields.Int(required=True)
    product_details = fields.Str(required=True, validate=validate.Length(min=1, max=200))
    order_status = fields.Str(required=True, validate=validate.OneOf(["待支付", "已支付", "制作中", "配送中", "已完成"]))
    created_at = fields.DateTime(dump_only=True)

class PaymentSchema(Schema):
    id = fields.Int(dump_only=True)
    order_id = fields.Int(required=True)
    payment_method = fields.Str(required=True, validate=validate.OneOf(["微信支付", "支付宝", "银行卡"]))
    payment_status = fields.Str(required=True, validate=validate.OneOf(["未支付", "已支付"]))
    created_at = fields.DateTime(dump_only=True)
