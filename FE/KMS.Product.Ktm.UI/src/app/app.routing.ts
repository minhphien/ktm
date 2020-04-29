import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { AuthGuard } from './_helpers';
import { MasterLayoutComponent } from './layouts/master-layout/master-layout.component';
import { AnonymousLayoutComponent } from './layouts/anonymous-layout/anonymous-layout.component';
import { ReportComponent } from './pages/report/report.component';

const authorizedRoutes: Routes = [
    { path: "", redirectTo: "home", pathMatch: "full" },
    { path: "home", component: HomeComponent},
    { path: "report", component: ReportComponent}
];

const routes: Routes = [
    {
        path: "",
        component: MasterLayoutComponent,
        canActivate: [AuthGuard],
        children: authorizedRoutes
    },
    {
        path: "",
        component: AnonymousLayoutComponent,
        children: [
            { path: "login", component: LoginComponent }
        ]
    },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const AppRoutingModule = RouterModule.forRoot(routes);