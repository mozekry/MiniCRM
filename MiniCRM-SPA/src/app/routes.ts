import { Routes } from "@angular/router";
import { CustomersComponent } from "./customers/customers.component";
import { HomeComponent } from "./home/home.component";
import { MenusComponent } from "./menus/menus.component";
import { OrdersComponent } from "./orders/orders.component";
import { UsersComponent } from "./users/users.component";
import { AuthGuard } from "./_guards/auth.guard";


export const appRoutes: Routes = [
    {path: "", component: HomeComponent},
    {
        path:'',
        runGuardsAndResolvers:'always',
        canActivate:[AuthGuard],
        children:[
            {path: "customers", component: CustomersComponent},
            {path: "menus", component: MenusComponent},
            {path: "orders", component: OrdersComponent},
            {path: "users", component: UsersComponent},
        ]
    },
    {path: "**", redirectTo:"",pathMatch:"full"}
];
