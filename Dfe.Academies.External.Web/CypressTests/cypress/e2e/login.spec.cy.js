import { url, login_username, login_password } from '../../config'
import CookieHeaderModal from '../page-objects/components/CookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'

describe('Login Success Test', () => {

  before(function() {
    cy.visit(url)
    CookieHeaderModal.clickAcceptAnalyticsCookies()
    A2BHome.StartNowVisible()
    A2BHome.clickStartNow()
  })

  it('should login into application', () => {
   // cy.origin('https://test-interactions.signin.education.gov.uk', () => {
   A2BLogin.login(login_username, login_password)
  })

  })
//})