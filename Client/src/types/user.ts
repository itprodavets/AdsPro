export type User = {
    id: string;
    username: string;
    isActive: boolean;
};

export type ListUsersQuery = {
    skipCount: number;
    maxResultCount: number;
}

export type UpdateUser = {
    username: string;
    isActive: boolean;
};

