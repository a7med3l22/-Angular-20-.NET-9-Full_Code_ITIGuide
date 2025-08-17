import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { handleReguestInterceptor } from './Interceptor/handle-reguest-interceptor';
import { provideStore } from '@ngrx/store';
import { AppReducer } from './state/app.reducer';
import { EffectsFeatureModule, EffectsModule, EffectsRootModule, provideEffects } from '@ngrx/effects';
import { LanguageEffects } from './language/store/langEffect';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withFetch(), withInterceptors([handleReguestInterceptor])),
    provideStore(AppReducer
    ),
    provideEffects([LanguageEffects])
  
]
};
/*
✅ الـ Function دي بترجع "حاجة من نوع" HttpFeature<HttpFeatureKind.Fetch>
يعني:
declare function withFetch(): HttpFeature<HttpFeatureKind.Fetch>;
المعنى هنا:

الدالة withFetch بترجع قيمة (value) أو كائن (object) من النوع HttpFeature<HttpFeatureKind.Fetch>

مش بترجع الكلاس نفسه (يعني مش بترجع blueprint أو constructor للكلاس)

📌 توضيح الفرق:
✅ 1. بترجع "حاجة من النوع":
ts
نسخ
تحرير
function createUser(): User {
  return new User(); // ده object من النوع User
}
❌ 2. لو كانت بترجع الكلاس نفسه:
ts
نسخ
تحرير
function getUserClass(): typeof User {
  return User; // الكلاس نفسه مش instance
}
✅ وبالتالي:
في حالتك:

ts
نسخ
تحرير
declare function withFetch(): HttpFeature<HttpFeatureKind.Fetch>;
يبقى الدالة بترجع object (instance) من النوع HttpFeature<HttpFeatureKind.Fetch>
وليس الكلاس أو الـ Type نفسه.
*/
