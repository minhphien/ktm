import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';

// used to create fake backend
import { fakeBackendProvider } from './_helpers';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing';
import { NgZorroAntdModule, NZ_ICONS, NzDrawerModule } from 'ng-zorro-antd';
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
import { TableComponent } from './components/home/table/table.component';
import { CreateKudosComponent } from './components/_shared/create-kudos/create-kudos.component';
import { LoaderComponent } from './components/_shared/loader/loader.component'// import { SidebarComponent } from './components/sidebar/sidebar.component';
import { appStateReducer } from './appState.reducer';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

registerLocaleData(en);
@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        NgZorroAntdModule,
        NzDrawerModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        StoreModule.forRoot({appstate: appStateReducer}),
        StoreDevtoolsModule.instrument({maxAge: 25})
    ],
    declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        FooterComponent ,
        MasterLayoutComponent ,
        AnonymousLayoutComponent ,
        UserInfoPanelComponent ,
        TableComponent ,
        CreateKudosComponent ,
        LoaderComponent  
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        { provide: NZ_I18N, useValue: en_US },
        // provider used to create fake backend
        fakeBackendProvider
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }