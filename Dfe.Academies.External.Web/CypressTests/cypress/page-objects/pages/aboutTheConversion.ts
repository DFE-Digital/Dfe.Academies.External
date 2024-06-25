class AboutTheConversion {
  // TODO get better selector for element
  public startContactDetails(): this {
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(0).click()

    return this
  }

  // TODO get better selectors for elements
  public checkAboutTheConversion(headTeacherName: string, headTeacherEmail: string,
    chairName: string, chairEmail: string, approverName: string, approverEmail: string): this {
    cy.get('.govuk-body').eq(0).contains('Name of headteacher')
    cy.get('.govuk-body').eq(1).contains(headTeacherName)

    cy.get('.govuk-body').eq(2).contains('Headteacher\'s email address')
    cy.get('.govuk-body').eq(3).contains(headTeacherEmail)

    cy.get('.govuk-body').eq(4).contains('Name of the chair of the governing body')
    cy.get('.govuk-body').eq(5).contains(chairName)

    cy.get('.govuk-body').eq(6).contains('Chair\'s email address')
    cy.get('.govuk-body').eq(7).contains(chairEmail)

    cy.get('.govuk-body').eq(8).contains('Who is the main contact for the conversion')
    cy.get('.govuk-body').eq(9).contains('The chair of the governing body')

    cy.get('.govuk-body').eq(10).contains('Approver\'s full name')
    cy.get('.govuk-body').eq(11).contains(approverName)

    cy.get('.govuk-body').eq(12).contains('Approver\'s email address')
    cy.get('.govuk-body').eq(13).contains(approverEmail)

    cy.get('.govuk-body').eq(14).contains('Do you want the conversion to happen on a particular date?')
    cy.get('.govuk-body').eq(15).contains('No')

    cy.get('.govuk-body').eq(16).contains('Why does the school want to join this trust in particular?')
    cy.get('.govuk-body').eq(17).contains('Reason to join this trust')

    cy.get('.govuk-body').eq(18).contains('Is the school planning to change its name when it becomes an academy?')
    cy.get('.govuk-body').eq(19).contains('No')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('[data-cy="saveAndReturnButton"]').click()

    return this
  }
}

const aboutTheConversion = new AboutTheConversion()

export default aboutTheConversion
