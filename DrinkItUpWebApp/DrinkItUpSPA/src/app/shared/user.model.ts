export class UserModel{
    userId?: number;
    userNameToShow?: string;
    email?: string;
    dateOfBirth?: Date;
    role?: RoleModel;
    token?: string;
}

export class RoleModel{
    roleId?: number;
    name?: string;
}

export class UserRegisterModel{
    constructor(
    public userNameToShow: string,
    public email: string,
    public password: string,
    public dateOfBirth: Date){}

}