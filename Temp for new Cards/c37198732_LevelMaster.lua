--レベル・マイスター
function c37198732.initial_effect(c)
	--Activate
	local e1=Effect.CreateEffect(c)
	e1:SetCategory(CATEGORY_LVCHANGE)
	e1:SetType(EFFECT_TYPE_ACTIVATE)
	e1:SetCode(EVENT_FREE_CHAIN)
	e1:SetCost(c37198732.cost)
	e1:SetTarget(c37198732.target)
	e1:SetOperation(c37198732.activate)
	c:RegisterEffect(e1)
end
function c37198732.costfilter(c)
	return c:IsAbleToGraveAsCost() and c:IsType(TYPE_MONSTER) 
	and Duel.IsExistingMatchingCard(c37198732.lvfilter,tp,LOCATION_MZONE,0,1,nil,c:GetOriginalLevel())
end
function c37198732.costfilter(c,lvl)
	return c:IsFaceup() and c:GetLevel()~=lvl
end
function c37198732.cost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.IsExistingMatchingCard(c37198732.costfilter,tp,LOCATION_HAND,0,1,nil) end
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_TOGRAVE)
	local g=Duel.SelectMatchingCard(tp,c37198732.costfilter,tp,LOCATION_HAND,0,1,1,nil)
	e:SetLabel(g:GetFirst():GetOriginalLevel())
	Duel.SendtoGrave(g,REASON_COST)
end
function c37198732.lvfilter(c,lv)
	return c:IsFaceup() and c:GetLevel()>0 and c:GetLevel()~=lvl
end
function c37198732.target(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return true end
	if chkc then return true end
	local lvl=e:GetLabel()
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_FACEUP)
	Duel.SelectTarget(tp,c37198732.lvfilter,tp,LOCATION_MZONE,0,1,2,nil,lvl)
end
function c37198732.activate(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	local g=Duel.GetChainInfo(0,CHAININFO_TARGET_CARDS)
	local lc=g:GetFirst()
	local lvl=e:GetLabel()
	while lc do
		if lc:IsRelateToEffect(e) and lc:IsFaceup() then
			local e1=Effect.CreateEffect(c)
			e1:SetType(EFFECT_TYPE_SINGLE)
			e1:SetCode(EFFECT_CHANGE_LEVEL)
			e1:SetValue(lvl)
			e1:SetReset(RESET_EVENT+0x1fe0000)
			lc:RegisterEffect(e1)
		end
		lc=g:GetNext()
	end
	
end
