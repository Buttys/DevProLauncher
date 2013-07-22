--聖蛇の息吹
function c98850929.initial_effect(c)
	--Activate
	local e1=Effect.CreateEffect(c)
	e1:SetCategory(CATEGORY_TOHAND)
	e1:SetType(EFFECT_TYPE_ACTIVATE)
	e1:SetProperty(EFFECT_FLAG_TARGET)
	e1:SetCode(EVENT_FREE_CHAIN)
	e1:SetCondition(c98850929.condition)
	e1:SetTarget(c98850929.target)
	e1:SetOperation(c98850929.activate)
	c:RegisterEffect(e1)
end

function c98850929.count()
	local g=Duel.GetMatchingGroup(Card.IsType,tp,LOCATION_MZONE,0,nil,TYPE_MONSTER)
	local c=g:GetFirst()
	local fus=0
	local syn=0
	local rit=0
	local xyz=0
	while c do
		if c:IsType(TYPE_FUSION) then
			fus=1
		end
		if c:IsType(TYPE_SYNCHRO) then
			syn=1
		end
		if c:IsType(TYPE_RITUAL) then
			rit=1
		end
		if c:IsType(TYPE_XYZ) then
			xyz=1
		end
		c=g:GetNext()
	end
	return fus+syn+rit+xyz
end

function c98850929.condition(e,tp,eg,ep,ev,re,r,rp)
	return c98850929.count()>1
end
function c98850929.filter(c,t)
	return c:IsType(t) and c:IsAbleToHand() and not c:IsCode(98850929)
end

function c98850929.target(e,tp,eg,ep,ev,re,r,rp,chk,chkc)
	if chkc==0 then return true end
	if chk==0 then return true end
	local count=c98850929.count()
	if count>1 and 
	( 
		chk==1 or Duel.IsExistingTarget(c98850929.filter,tp,LOCATION_GRAVE+LOCATION_REMOVED,0,1,nil,TYPE_MONSTER)
	)
	then
		Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_ATOHAND)
		local g=Duel.SelectTarget(tp,c98850929.filter,tp,LOCATION_GRAVE+LOCATION_REMOVED,0,1,1,nil,TYPE_MONSTER)
		Duel.SetOperationInfo(0,CATEGORY_TOHAND,g,g:GetCount(),0,0)
	end
	if count>2 and 
	( 
		chk==1 or Duel.IsExistingTarget(c98850929.filter,tp,LOCATION_GRAVE,0,1,nil,TYPE_TRAP)
	)
	then
		Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_ATOHAND)
		local g1=Duel.SelectTarget(tp,c98850929.filter,tp,LOCATION_GRAVE,0,1,1,nil,TYPE_TRAP)
		Duel.SetOperationInfo(0,CATEGORY_TOHAND,g1,g1:GetCount(),0,0)
	end
	if count==4 and 
	( 
		chk==1 or Duel.IsExistingTarget(c98850929.filter,tp,LOCATION_GRAVE,0,1,nil,TYPE_SPELL)
	)
	then
		Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_ATOHAND)
		local g2=Duel.SelectTarget(tp,c98850929.filter,tp,LOCATION_GRAVE,0,1,1,nil,TYPE_SPELL)
		Duel.SetOperationInfo(0,CATEGORY_TOHAND,g2,g2:GetCount(),0,0)
	end
end
function c98850929.activate(e,tp,eg,ep,ev,re,r,rp)
	local g=Duel.GetChainInfo(0,CHAININFO_TARGET_CARDS)
	local tg=g:Filter(Card.IsRelateToEffect,nil,e)
	if tg:GetCount()>0 then
		Duel.SendtoHand(tg,nil,REASON_EFFECT)
		Duel.ConfirmCards(1-tp,tg)
	end
end
