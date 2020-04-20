import { User } from './user';
export class Employee extends User {
    employeeId: number;
    joinDate: Date;
    currentOfficeId: number;
    currentOffice: string;
    departmentId: number;
    departmentName: string;
    projectId: number;
    projectName: string;
    clientId: number;
    clientName: string;
    resourceId: number;
    resource: string;
    titleName: string;
    titleShortName: string;
    phone: string;
    gender: number;
    avatarUrl: string;
    imgUrl: string;
}
