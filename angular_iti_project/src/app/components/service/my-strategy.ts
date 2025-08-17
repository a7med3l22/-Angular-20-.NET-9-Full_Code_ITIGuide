import { ActivatedRouteSnapshot, DetachedRouteHandle, RouteReuseStrategy } from "@angular/router";

export class MyStrategy extends RouteReuseStrategy {
  override shouldReuseRoute(future: ActivatedRouteSnapshot, curr: ActivatedRouteSnapshot): boolean {
    return false; // لازم false علشان يعمل إعادة تحميل حقيقي
  }

  override shouldDetach(): boolean {
    return false;
  }

  override store(): void {}

  override shouldAttach(): boolean {
    return false;
  }

  override retrieve(): DetachedRouteHandle | null {
    return null;
  }
}
