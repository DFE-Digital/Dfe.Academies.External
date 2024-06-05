class MainContacts {
  public mainContactsNotStartedElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Conversion key details')
    cy.get('.govuk-heading-l').contains('Main contacts')

    cy.get('.govuk-label').eq(0).contains('Name of headteacher')
    cy.get('#ViewModel\\.ContactHeadName').should('be.enabled')

    cy.get('.govuk-label').eq(1).contains('Headteacher\'s email address')
    cy.get('.govuk-hint').eq(0).contains('We will only use this to contact you regarding your application')
    cy.get('#ViewModel\\.ContactHeadEmail').should('be.enabled')

    cy.get('.govuk-label').eq(2).contains('Name of the chair of the governing body')
    cy.get('#ViewModel\\.ContactChairName').should('be.enabled')

    cy.get('.govuk-label').eq(3).contains('Chair\'s email address')
    cy.get('.govuk-hint').eq(1).contains('We will only use this to contact you regarding your application')
    cy.get('#ViewModel\\.ContactChairEmail').should('be.enabled')

    cy.get('span[class=govuk-fieldset__legend govuk-fieldset__legend--s]').contains('Who is the main contact for the conversion?')

    cy.get('#ContactTypeHeadTeacher').should('not.be.checked')
    cy.get('label[for=ContactTypeHeadTeacher]').contains('The headteacher')

    cy.get('#ContactTypeChairOfGoverningBody').should('not.be.checked')
    cy.get('label[for=ContactTypeChairOfGoverningBody]').contains('The chair of the governing body')

    cy.get('#ContactTypeOther').should('not.be.checked')
    cy.get('label[for=ContactTypeOther]').contains('Someone else')

    cy.get('.govuk-fieldset__heading').contains('When your schools converts, we need to create a new DfE sign-in account for the academy. Please provide the most suitable contact to manage the new academies account.')

    cy.get('.govuk-hint').eq(3).contains('For more details on the approvers role and responsibilities please see')
    cy.get('a[href="https://help.signin.education.gov.uk/contact/approver"]').contains('the approver guide')

    cy.get('label[for=ApproverContactName]').contains('Full Name')
    cy.get('#ApproverContactName').should('be.enabled')

    cy.get('label[for=ApproverContactEmail]').contains('Email address')
    cy.get('.govuk-hint').eq(4).contains('We will only use this to contact you regarding your application')
    cy.get('#ApproverContactEmail').should('be.enabled')

    cy.get('input[type=submit]').should('be.visible').contains('Save and continue')

    return this
  }

  public fillMainContactDetailsAndSubmit(headTeacherName: string, headTeacherEmail: string,
    chairName: string, chairEmail: string, approverName: string, approverEmail: string): this {
    cy.get('#ViewModel\\.ContactHeadName').type(headTeacherName)
    cy.get('#ViewModel\\.ContactHeadEmail').type(headTeacherEmail)

    cy.get('#ViewModel\\.ContactChairName').type(chairName)
    cy.get('#ViewModel\\.ContactChairEmail').type(chairEmail)

    cy.get('#ContactTypeChairOfGoverningBody').click()
    cy.get('#ContactTypeChairOfGoverningBody').should('be.checked')

    cy.get('#ApproverContactName').type(approverName)
    cy.get('#ApproverContactEmail').type(approverEmail)
    cy.get('input[type=submit]').click()

    return this
  }
}

const mainContacts = new MainContacts()

export default mainContacts
