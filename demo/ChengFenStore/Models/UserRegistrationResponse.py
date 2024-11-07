class UserRegistrationResponse:
    def __init__(self, success, message, token):
        self.success = success
        self.message = message
        self.token = token
