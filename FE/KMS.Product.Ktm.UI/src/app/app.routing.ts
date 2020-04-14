import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './pages/home';
import { LoginComponent } from './pages/login';
import { AuthGuard } from './_helpers';
import { UserProfileComponent } from './pages/user-profile/user-profile.component';
import { MasterLayoutComponent } from './layouts/master-layout/master-layout.component';
import { AnonymousLayoutComponent } from './layouts/anonymous-layout/anonymous-layout.component';

const authorizedRoutes: Routes = [
    { path: "", redirectTo: "home", pathMatch: "full" },
    { path: "home", component: HomeComponent, canActivate: [AuthGuard] },
    { path: "user-profile", component: UserProfileComponent, canActivate: [AuthGuard] }
];

const routes: Routes = [
    {
        path: "",
        component: MasterLayoutComponent,
        canActivate: [AuthGuard],
        children: authorizedRoutes
    },
    {
        path: '',
        component: AnonymousLayoutComponent,
        children: [
            { path: "login", component: LoginComponent }
        ]
    },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);