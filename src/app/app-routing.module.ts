import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {NavigationComponent} from "./navigation/navigation.component";
import {WatermarkComponent} from "./watermark/watermark.component";
import {CompressComponent} from "./compress/compress.component";

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full'},
  { path: 'home', component: NavigationComponent,  },
  { path: 'home/watermark', component: WatermarkComponent },
  { path: 'home/compress', component: CompressComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
