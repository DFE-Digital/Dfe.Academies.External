import { url } from '../../config'
import CookieHeaderModal from '../page-objects/components/CookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BCreateAnAccount from '../page-objects/pages/A2BCreateAnAccount'

describe('Create Account Tests', () => {

  beforeEach(function() {
    cy.visit(url)
    CookieHeaderModal.clickAcceptAnalyticsCookies()
    A2BHome.StartNowVisible()
    A2BHome.clickStartNow()
  })

  it('should go to Create Account Page', () => {
    A2BLogin.createAccount()
    A2BCreateAnAccount.createAccountElementsVisible()
   })


})