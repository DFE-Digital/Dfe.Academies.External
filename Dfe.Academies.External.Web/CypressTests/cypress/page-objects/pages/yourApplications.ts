class YourApplications {
  public yourApplicationsElementsVisible(): this {
    // TODO Remove reliance on wait
    // eslint-disable-next-line cypress/no-unnecessary-waiting
    cy.wait(3000)
    cy.get('h1').contains('Your applications')
    cy.get('h2').contains('Applications in progress')
    cy.get('th:nth-child(1)').contains('Application')
    cy.get('th:nth-child(2)').contains('Trust Name')
    cy.get('th:nth-child(3)').contains('School Or Schools Applying To Convert')
    cy.get('a[href="/what-are-you-applying-to-do"]').should('be.visible').contains('Start a new application')

    return this
  }

  public selectJAMNotStartedApplicationButSchoolAdded(): this {
    cy.get('a[href="/application-overview?appId=10048"]').contains('Join a multi-academy trust')
    cy.get('ul').contains('Plymstock School')
    cy.get('a[href="/application-overview?appId=10048"]').click()

    return this
  }

  public selectStartANewApplication(): this {
    cy.get('a[href="/what-are-you-applying-to-do"]').click()

    return this
  }

  public verifyApplicationDeleted(applicationId: string): this {
    cy.log(`Global Application ID = ${applicationId}`)

    cy.get('.govuk-body').eq(0).contains(`trust A2B_${applicationId} has been successfully removed.`)

    cy.get('.govuk-table').contains(`${applicationId}`).should('not.exist')

    return this
  }

  public selectApplicationForInviteContributor(): this {
    cy.contains('10280').click()

    return this
  }
}

const yourApplications = new YourApplications()

export default yourApplications
