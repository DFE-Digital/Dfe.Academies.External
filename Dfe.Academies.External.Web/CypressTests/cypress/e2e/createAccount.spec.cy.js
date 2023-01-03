import { url } from '../../config'
import CookieHeaderModal from '../page-objects/components/CookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BCreateAnAccount from '../page-objects/pages/A2BCreateAnAccount'
import DataGenerator from '../fixtures/data-generator'
import A2BCreateAnAccountConfirm from '../page-objects/pages/A2BCreateAnAccountConfirm'

describe('Create Account Tests', () => {

  beforeEach(function() {
    cy.visit(url)
    CookieHeaderModal.clickAcceptAnalyticsCookies()
    A2BHome.StartNowVisible()
    A2BHome.clickStartNow()
  })

  it('should fail to create an account just clicking submit', () => {
    A2BLogin.createAccount()
    A2BCreateAnAccount.createAccountElementsVisible()
    A2BCreateAnAccount.createAccountFailsWithNoData()
  })

  it('should Create an Account Successfully', () => {
    A2BLogin.createAccount()
    A2BCreateAnAccount.createAccountElementsVisible()
    A2BCreateAnAccount.createAccountSuccessful()
    A2BCreateAnAccountConfirm.createAccountConfirmElementsVisible()
   })


})