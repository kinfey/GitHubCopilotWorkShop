class UserService:
    def __init__(self, user_repository, configuration):
        self.user_repository = user_repository
        self.configuration = configuration

    def register(self, request):
        existing_user = self.user_repository.get_user_by_phone_number(request.phone_number)
        if existing_user:
            return {
                "success": False,
                "message": "User already exists."
            }

        user = {
            "phone_number": request.phone_number,
            "name": request.name,
            "password": request.password
        }

        self.user_repository.add_user(user)

        return {
            "success": True,
            "message": "User registered successfully."
        }

    def login(self, request):
        user = self.user_repository.get_user_by_phone_number(request.phone_number)
        if not user or user["password"] != request.password:
            return {
                "success": False,
                "message": "Invalid phone number or password."
            }

        token = self.generate_jwt_token(user)

        return {
            "success": True,
            "message": "Login successful.",
            "token": token
        }

    def get_user_profile(self, user_id):
        user = self.user_repository.get_user_by_id(user_id)
        if not user:
            return None

        return {
            "phone_number": user["phone_number"],
            "name": user["name"],
            "orders": user["orders"]
        }

    def generate_jwt_token(self, user):
        import jwt
        import datetime

        payload = {
            "user_id": user["phone_number"],
            "exp": datetime.datetime.utcnow() + datetime.timedelta(days=7)
        }
        key = self.configuration["Jwt"]["Key"]
        token = jwt.encode(payload, key, algorithm="HS256")

        return token
