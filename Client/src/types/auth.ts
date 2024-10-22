export type UserLoginRequest = {
    username: string;
    password: string;
};

export type UserLoginResponse = {
    token: string;
};