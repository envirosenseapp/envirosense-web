export interface SignInRequest {
  email: string;
  password: string;
}

export interface SignInResponse {
  token: string;
}

export interface SignUpRequest {
  email: string;
  password: string;
  confirmPassword: string;
}

export interface SignUpResponse {
  id: string;
  email: string;
  createdAt: string;
  updatedAt: string;
}
