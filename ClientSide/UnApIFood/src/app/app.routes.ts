import { Routes } from '@angular/router';
import { LoginComponent } from './component/login/login.component';
import { MainComponent } from './component/main/main.component';

export const routes: Routes = [
    {
        path: '', redirectTo: 'login', pathMatch: 'full'
    },
    {
        path: 'login',
        component:LoginComponent
    },
    {
        path: 'main',
        component:MainComponent,
        children: [
            {
                path: 'main',
                component:MainComponent
            }
        ]
    }
];
