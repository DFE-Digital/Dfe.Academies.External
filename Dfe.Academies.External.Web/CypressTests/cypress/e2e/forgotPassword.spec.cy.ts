import { url } from '../../config'
import Header from '../page-objects/components/Header'
import CookieHeaderModal from '../page-objects/components/CookieHeaderModal'
import A2BHome from '../page-objects/pages/A2BHome'
import A2BLogin from '../page-objects/pages/A2BLogin'
import A2BForgottenYourPassword from '../page-objects/pages/A2BForgottenYourPassword'
import A2BForgottenYourPasswordVerifyCode from '../page-objects/pages/A2BForgottenYourPasswordVerifyCode'
import Footer from '../page-objects/components/Footer'

describe('Forgotten Password Tests', () => {

  beforeEach(function() {
    cy.visit(url)

    Header.govUkHeaderVisible()
    Header.applyToBecomeAnAcademyHeaderLinkVisible()

    A2BHome.homePageElementsVisible()

    Footer.checkFooterLinksVisible()

    CookieHeaderModal.clickAcceptAnalyticsCookies()
    
    A2BHome.clickStartNow()
  })

  it('should validate Empty Email Submission On Forgot Password Page', () => {
    A2BLogin.forgotPassword()

    A2BForgottenYourPassword.forgotPasswordElementsVisible()

    A2BForgottenYourPassword.forgotPasswordEmptyEmailSubmitted()
   })

  it('should validate an Invalid Email Submission On Forgot Password Page', () => {
    A2BLogin.forgotPassword()

    A2BForgottenYourPassword.forgotPasswordElementsVisible()

    A2BForgottenYourPassword.forgotPasswordInvalidEmailSubmitted()
    })

    it('should go to Forgotten Password Confirmation Page for an Apply to Become User', () => {
    A2BLogin.forgotPassword()

    A2BForgottenYourPassword.forgotPasswordElementsVisible()

    A2BForgottenYourPassword.forgotPasswordA2BUserEmailSubmitted()

    A2BForgottenYourPasswordVerifyCode.forgotPasswordVerifyCodeElementsVisible()



    })


})