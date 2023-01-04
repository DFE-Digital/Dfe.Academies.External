import { url, login_username, login_password } from '../../config'
import CookieHeaderModal from '../page-objects/components/CookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BYourApplications from '../page-objects/pages/A2BYourApplications'

describe('Login Tests', () => {

  beforeEach(function() {
    cy.visit(url)
    CookieHeaderModal.clickAcceptAnalyticsCookies()
    A2BHome.StartNowVisible()
    A2BHome.clickStartNow()
  })

  it('should NOT login into application WITH NO PASSWORD', () => {
    A2BLogin.loginWithNoPassword(login_username, '')
    A2BLogin.loginErrorVisibleWithNoPassword()
  })

  it('should NOT login into application WITH NO USERNAME', () => {
    A2BLogin.loginWithNoUsername(login_username, login_password)
    A2BLogin.loginErrorVisibleWithNoUsername()
  })

  it('should NOT login into application WITH NO USERNAME OR PASSWORD', () => {
    A2BLogin.loginWithNoUsernameOrPassword(login_username, login_password)
    A2BLogin.loginErrorVisibleWithNoPassword()
  })

  it('should NOT login into application WITH INCORRECT PASSWORD', () => {
    A2BLogin.loginWithWrongPassword(login_username, 'POTATO')
    A2BLogin.loginErrorVisibleWithWrongPassword()
  })

  it('should NOT login into application WITH INCORRECT USERNAME', () => {
    A2BLogin.loginWithWrongUsername('rachel.riley@msn.com', login_password)
    A2BLogin.loginErrorVisibleWithWrongUsername()
  })

  it('should NOT login into application WITH SQL INJECTION IN FIELDS / INVALID USERNAME WITH NO PASSWORD', () => {
    A2BLogin.sqlInjectionAndInvalidUsername(login_username, login_password)
    A2BLogin.loginErrorVisibleWithSqlInjectionAttemptAndInvalidUsername()
})

it('should NOT login into application WITH CROSS-SITE SCRIPTING ATTEMPT / INVALID USERNAME WITH NO PASSWORD', () => {
  A2BLogin.crossSiteScriptAndInvalidUsername(login_username, login_password)
  A2BLogin.loginErrorVisibleWithCrossSiteScriptAndInvalidUsername()
})

  it('should login into application WITH CORRECT CREDENTIALS', () => {
   A2BLogin.login(login_username, login_password)
   A2BYourApplications.yourApplicationsElementsVisible()
  })

})
