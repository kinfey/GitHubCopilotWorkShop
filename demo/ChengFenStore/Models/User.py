from ..Data.AppDbContext import db

class User(db.Model):
    phone_number = db.Column(db.String(20), primary_key=True)
    name = db.Column(db.String(100), nullable=False)
    password = db.Column(db.String(100), nullable=False)
