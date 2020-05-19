import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { AuthGuard } from './_helpers';
import { MasterLayoutComponent } from './layouts/master-layout/master-layout.component';
import { AnonymousLayoutComponent } from './layouts/anonymous-layout/anonymous-layout.component';
import { ReportLayoutComponent } from './layouts/master-layout/report-layout/report-layout.component';
import { KudosByTeamFilterComponent } from './components/report/kudos-by-team-filter/kudos-by-team-filter.component';
import { KudosByTeamComponent } from './pages/report/kudos-by-team/kudos-by-team.component';
import { Error404Component } from './pages/error404/error404.component';
import { KudosAcrossTeamComponent } from './pages/report/kudos-across-team/kudos-across-team.component';
import { KudosAcrossTeamFilterComponent } from './components/report/kudos-across-team-filter/kudos-across-team-filter.component';


const reportRoute: Routes = [
    { path: "", redirectTo: "kudos-by-team", pathMatch: "full" },
    { 
        path: "kudos-by-team", 
        children: [
            { path: "", component: KudosByTeamComponent, pathMatch: "full" },
            { path: "", component: KudosByTeamFilterComponent, outlet: "filter" },
            { path: ":userName", component: KudosByTeamComponent, pathMatch: "full" }
        ]
    },
    { 
        path: "kudos-across-team", 
        children: [
            { path: "", component: KudosAcrossTeamComponent, pathMatch: "full" },
            { path: "", component: KudosAcrossTeamFilterComponent, outlet: "filter" }
        ]
    },
];
const authorizedRoutes: Routes = [
    { path: "", redirectTo: "home", pathMatch: "prefix" },
    { path: "home", component: HomeComponent},
    { path: "report", component: ReportLayoutComponent, pathMatch: "prefix", children: reportRoute }
];

const routes: Routes = [
    {
        path: "",
        component: MasterLayoutComponent,
        canActivate: [AuthGuard],
        children: authorizedRoutes,
        pathMatch: "prefix"
    },
    {
        path: "",
        component: AnonymousLayoutComponent,
        children: [
            { path: "login", component: LoginComponent }            
        ]
    },
    { path: "404", component: Error404Component},
    { path: '**', redirectTo: "404" }
];

export const AppRoutingModule = RouterModule.forRoot(routes);