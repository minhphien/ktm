import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';

// used to create fake backend
import { fakeBackendProvider } from './_helpers';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing';
import { NgZorroAntdModule, NzDrawerModule } from 'ng-zorro-antd';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { NZ_I18N, en_US } from 'ng-zorro-antd/i18n';

import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { FooterComponent } from './components/_shared/footer/footer.component';
import { AnonymousLayoutComponent } from './layouts/anonymous-layout/anonymous-layout.component';
import { MasterLayoutComponent } from './layouts/master-layout/master-layout.component';;
import { UserInfoPanelComponent } from './components/home/user-info-panel/user-info-panel.component';
import { TableKudosReceivedComponent } from './components/home/table-kudos-recevied/table-kudos-recevied.component';
import { CreateKudosComponent } from './components/_shared/create-kudos/create-kudos.component';
import { LoaderComponent } from './components/_shared/loader/loader.component'
import { appStateReducer } from './appState.reducer';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { VarDirective } from './_directives/varDirective';
import { kudosStateReducer } from './_reducers/kudos-list.reducer';
import { AppreviationPipe } from './_pipes/appreviation.pipe';
import { EmojiCssPipe } from "./_pipes/emojicss.pipe";
import { TableKudosSentComponent } from './components/home/table-kudos-sent/table-kudos-sent.component';
import { QuillModule } from 'ngx-quill';
import { AvatarComponent } from './components/_shared/avatar/avatar.component';
import { AppService } from './_services/app.service';
import { ReportLayoutComponent } from './layouts/master-layout/report-layout/report-layout.component';
import { KudosByTeamFilterComponent } from './components/report/kudos-by-team-filter/kudos-by-team-filter.component'
import { KudosByTeamComponent } from './pages/report/kudos-by-team/kudos-by-team.component';
import { KudosByTeamEmployeeComponent } from './pages/report/kudos-by-team-employee/kudos-by-team-employee.component';
import { Error404Component } from './pages/error404/error404.component'

registerLocaleData(en);
@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        FormsModule,
        NgZorroAntdModule,
        NzDrawerModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        StoreModule.forRoot({appstate: appStateReducer, kudosState: kudosStateReducer}),
        StoreDevtoolsModule.instrument({maxAge: 25}),
        QuillModule.forRoot({
            theme: 'bubble'
        })      
    ],
    declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        FooterComponent,
        MasterLayoutComponent,
        AnonymousLayoutComponent,
        UserInfoPanelComponent,
        TableKudosReceivedComponent, 
        TableKudosSentComponent,
        CreateKudosComponent ,
        LoaderComponent,
        VarDirective,
        AppreviationPipe,
        EmojiCssPipe,
        KudosByTeamComponent,
        ReportLayoutComponent,
        AvatarComponent ,
        ReportLayoutComponent ,
        KudosByTeamFilterComponent ,
        KudosByTeamEmployeeComponent,
        Error404Component
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        { provide: NZ_I18N, useValue: en_US },
        // provider used to create fake backend
        fakeBackendProvider,
        AppService
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }