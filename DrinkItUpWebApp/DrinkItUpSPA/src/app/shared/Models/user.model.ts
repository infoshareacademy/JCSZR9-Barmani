export class UserModel{
    userId?: number;
    userNameToShow?: string;
    email?: string;
    dateOfBirth?: Date;
    roleId?: number;
    role?: RoleModel;
    token?: string;
}

export class RoleModel{
    roleId?: number;
    name?: string;
    isUsed?: boolean;
}

export class UserRegisterModel{
    constructor(
    public userNameToShow: string,
    public email: string,
    public password: string,
    public dateOfBirth: Date){}

}