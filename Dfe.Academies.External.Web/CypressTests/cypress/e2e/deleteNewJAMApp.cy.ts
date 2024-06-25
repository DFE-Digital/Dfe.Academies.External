import cookieHeaderModal from '../page-objects/components/cookieHeaderModal'
import home from '../page-objects/pages/home'
import login from '../page-objects/pages/login'
import yourApplications from '../page-objects/pages/yourApplications'
import whatAreYouApplyingToDo from '../page-objects/pages/whatAreYouApplyingToDo'
import whatIsYourRole from '../page-objects/pages/whatIsYourRole'
import application from '../page-objects/pages/application'
import confirmApplicationDelete from '../page-objects/pages/confirmApplicationDelete'

describe('Delete application', () => {
  beforeEach(function () {
    cy.visit(Cypress.env('url'))

    cookieHeaderModal.acceptAnalyticsCookies()

    home.start()

    login.login()
  })

  it('should be able to delete a JAM application', () => {
    yourApplications.startNewApplication()

    whatAreYouApplyingToDo.startApplication('Join A MAT')

    whatIsYourRole.chooseRole('Governor')

    application.selectCancelApplication()

    confirmApplicationDelete.confirmDelete()

    yourApplications.verifyApplicationDeleted()
  })
})
