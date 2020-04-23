import { Role } from "./role";

export class User {
    id: number;
    employeeCode: string;
    firstName: string;
    lastName: string;
    middleName: string;
    displayName: string;
    userName: string;
    email: string;
    token?: string;
    fullName: string;
    shortName: string;
}

