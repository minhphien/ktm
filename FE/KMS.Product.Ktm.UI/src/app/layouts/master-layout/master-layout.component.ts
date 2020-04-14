import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '@app/_services';
import { User } from '@app/_models';
import { first } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-master-layout',
  templateUrl: './master-layout.component.html',
  styleUrls: ['./master-layout.component.less']
})
export class MasterLayoutComponent implements OnInit {
  currentUser: User;
  loading = false;
  menus = [
    {
      name: 'Home',
      path: '/home'
    },
    {
      name: 'User info',
      path: '/user-profile'
    }
  ]

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
  ) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  ngOnInit() {
    const urlParams = new URLSearchParams(window.location.search);
    const accessToken = urlParams.get('accessToken');

    if (accessToken) {
      this.authenticationService.login(accessToken).pipe(first()).subscribe(() => {
        this.loading = false;
        this.router.navigate([window.location.pathname]);
      });
    } else {
      window.location.replace(`https://home.kms-technology.com/login?returnUrl=${window.location.href}`)
    }
  }

}
