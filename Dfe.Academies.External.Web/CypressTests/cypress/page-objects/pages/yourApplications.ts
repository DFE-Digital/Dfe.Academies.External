class YourApplications {
  public yourApplicationsElementsVisible(): this {
    cy.yourApplicationsElementsVisible()

    return this
  }

  public selectJAMNotStartedApplicationButSchoolAdded(): this {
    cy.selectJAMNotStartedApplicationButSchoolAdded()

    return this
  }

  public selectStartANewApplication(): this {
    cy.get('a[href=/what-are-you-applying-to-do]').click()

    return this
  }

  public verifyApplicationDeleted(): this {
    cy.verifyApplicationDeleted()

    return this
  }

  public selectApplicationForInviteContributor(): this {
    cy.contains('10280').click()

    return this
  }
}

const yourApplications = new YourApplications()

export default yourApplications
