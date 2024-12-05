class UserRegistrationRequest:
    def __init__(self, phone_number, name, password, verification_code):
        self.phone_number = phone_number
        self.name = name
        self.password = password
        self.verification_code = verification_code
