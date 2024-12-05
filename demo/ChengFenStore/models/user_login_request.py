class UserLoginRequest:
    def __init__(self, phone_number, password, verification_code):
        self.phone_number = phone_number
        self.password = password
        self.verification_code = verification_code
